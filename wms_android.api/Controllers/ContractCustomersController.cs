using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wms_android.shared.Data;
using wms_android.shared.Models;
using wms_android.shared.DTOs;

namespace wms_android.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractCustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContractCustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ContractCustomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractCustomer>>> GetContractCustomers()
        {
            try
            {
                var customers = await _context.ContractCustomers
                    .Include(c => c.CreatedBy)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve contract customers", error = ex.Message });
            }
        }

        // GET: api/ContractCustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractCustomer>> GetContractCustomer(int id)
        {
            try
            {
                var customer = await _context.ContractCustomers
                    .Include(c => c.CreatedBy)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                {
                    return NotFound(new { message = "Contract customer not found" });
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve contract customer", error = ex.Message });
            }
        }

        // POST: api/ContractCustomers
        [HttpPost]
        public async Task<ActionResult<ContractCustomer>> CreateContractCustomer(CreateContractCustomerDto createDto)
        {
            try
            {
                // Check if customer with same name or email already exists
                var existingCustomer = await _context.ContractCustomers
                    .Where(c => c.Name.ToLower() == createDto.Name.ToLower() || 
                               (c.Email != null && createDto.Email != null && c.Email.ToLower() == createDto.Email.ToLower()))
                    .FirstOrDefaultAsync();

                if (existingCustomer != null)
                {
                    return BadRequest(new { message = "Customer with this name or email already exists" });
                }

                var customer = new ContractCustomer
                {
                    Name = createDto.Name,
                    CompanyName = createDto.CompanyName,
                    Email = createDto.Email,
                    Phone = createDto.Phone,
                    Address = createDto.Address,
                    ContactPerson = createDto.ContactPerson,
                    ContractNumber = !string.IsNullOrWhiteSpace(createDto.ContractNumber) 
                        ? createDto.ContractNumber 
                        : await GenerateContractNumberAsync(),
                    PaymentTerms = createDto.PaymentTerms ?? "Net 30",
                    TaxRate = createDto.TaxRate,
                    IsActive = true,
                    CreatedById = GetCurrentUserId(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.ContractCustomers.Add(customer);
                await _context.SaveChangesAsync();

                // Reload with relationships
                var createdCustomer = await _context.ContractCustomers
                    .Include(c => c.CreatedBy)
                    .FirstAsync(c => c.Id == customer.Id);

                return CreatedAtAction(nameof(GetContractCustomer), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to create contract customer", error = ex.Message });
            }
        }

        // PUT: api/ContractCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContractCustomer(int id, UpdateContractCustomerDto updateDto)
        {
            try
            {
                var customer = await _context.ContractCustomers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound(new { message = "Contract customer not found" });
                }

                // Update properties
                if (!string.IsNullOrEmpty(updateDto.Name))
                    customer.Name = updateDto.Name;
                
                if (!string.IsNullOrEmpty(updateDto.CompanyName))
                    customer.CompanyName = updateDto.CompanyName;
                
                if (!string.IsNullOrEmpty(updateDto.Email))
                    customer.Email = updateDto.Email;
                
                if (!string.IsNullOrEmpty(updateDto.Phone))
                    customer.Phone = updateDto.Phone;
                
                if (!string.IsNullOrEmpty(updateDto.Address))
                    customer.Address = updateDto.Address;
                
                if (!string.IsNullOrEmpty(updateDto.ContactPerson))
                    customer.ContactPerson = updateDto.ContactPerson;
                
                if (!string.IsNullOrEmpty(updateDto.ContractNumber))
                    customer.ContractNumber = updateDto.ContractNumber;
                
                if (!string.IsNullOrEmpty(updateDto.PaymentTerms))
                    customer.PaymentTerms = updateDto.PaymentTerms;
                
                if (updateDto.TaxRate.HasValue)
                    customer.TaxRate = updateDto.TaxRate.Value;
                
                if (updateDto.IsActive.HasValue)
                    customer.IsActive = updateDto.IsActive.Value;

                customer.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to update contract customer", error = ex.Message });
            }
        }

        // DELETE: api/ContractCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractCustomer(int id)
        {
            try
            {
                var customer = await _context.ContractCustomers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound(new { message = "Contract customer not found" });
                }

                // Check if customer has any invoices
                var hasInvoices = await _context.Invoices.AnyAsync(i => i.ContractCustomerId == id);
                if (hasInvoices)
                {
                    // Soft delete - just deactivate
                    customer.IsActive = false;
                    customer.UpdatedAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Hard delete if no invoices
                    _context.ContractCustomers.Remove(customer);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to delete contract customer", error = ex.Message });
            }
        }

        // GET: api/ContractCustomers/5/invoices
        [HttpGet("{id}/invoices")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetCustomerInvoices(int id)
        {
            try
            {
                var customer = await _context.ContractCustomers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound(new { message = "Contract customer not found" });
                }

                var invoices = await _context.Invoices
                    .Include(i => i.Items)
                    .Include(i => i.CreatedBy)
                    .Where(i => i.ContractCustomerId == id)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();

                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve customer invoices", error = ex.Message });
            }
        }

        private int GetCurrentUserId()
        {
            // TODO: Implement proper authentication and get user ID from JWT token
            // For now, return a default user ID
            return 1; // Admin user
        }

        private async Task<string> GenerateContractNumberAsync()
        {
            var currentYear = DateTime.UtcNow.Year;
            var yearPrefix = $"FHL-{currentYear}-CTR";

            // Get the highest numeric code for this year
            var existingContracts = await _context.ContractCustomers
                .Where(c => c.ContractNumber != null && c.ContractNumber.StartsWith(yearPrefix))
                .Select(c => c.ContractNumber)
                .ToListAsync();

            int maxNumber = 0;
            foreach (var contractNumber in existingContracts)
            {
                // Extract the numeric part after the prefix
                var numericPart = contractNumber.Substring(yearPrefix.Length);
                if (int.TryParse(numericPart, out int number))
                {
                    maxNumber = Math.Max(maxNumber, number);
                }
            }

            // Generate the next number with zero-padding (3 digits)
            var nextNumber = maxNumber + 1;
            return $"{yearPrefix}{nextNumber:D3}";
        }
    }
}