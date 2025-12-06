using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wms_android.shared.Data;
using wms_android.shared.Models;
using wms_android.shared.DTOs;

namespace wms_android.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            try
            {
                var invoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.CreatedBy)
                    .Include(i => i.Items)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();

                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve invoices", error = ex.Message });
            }
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.CreatedBy)
                    .Include(i => i.Items)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found" });
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve invoice", error = ex.Message });
            }
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(CreateInvoiceDto createInvoiceDto)
        {
            try
            {
                // Validate contract customer exists
                var customer = await _context.ContractCustomers.FindAsync(createInvoiceDto.ContractCustomerId);
                if (customer == null)
                {
                    return BadRequest(new { message = "Contract customer not found" });
                }

                // Generate invoice number
                var invoiceNumber = await GenerateInvoiceNumber();

                var invoice = new Invoice
                {
                    InvoiceNumber = invoiceNumber,
                    CustomerId = createInvoiceDto.ContractCustomerId,
                    ContractCustomerId = createInvoiceDto.ContractCustomerId,
                    IssueDate = createInvoiceDto.IssueDate,
                    DueDate = createInvoiceDto.DueDate,
                    BillingPeriodStart = createInvoiceDto.BillingPeriodStart,
                    BillingPeriodEnd = createInvoiceDto.BillingPeriodEnd,
                    Status = "draft",
                    Subtotal = 0,
                    TaxAmount = 0,
                    TotalAmount = 0,
                    PaidAmount = 0,
                    Notes = createInvoiceDto.Notes,
                    CreatedById = createInvoiceDto.CreatedById,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

                // Reload with relationships
                var createdInvoice = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.CreatedBy)
                    .Include(i => i.Items)
                    .FirstAsync(i => i.Id == invoice.Id);

                return CreatedAtAction(nameof(GetInvoice), new { id = createdInvoice.Id }, createdInvoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to create invoice", error = ex.Message });
            }
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, UpdateInvoiceDto updateInvoiceDto)
        {
            try
            {
                var invoice = await _context.Invoices.FindAsync(id);
                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found" });
                }

                // Update properties
                if (updateInvoiceDto.IssueDate.HasValue)
                    invoice.IssueDate = updateInvoiceDto.IssueDate.Value;
                
                if (updateInvoiceDto.DueDate.HasValue)
                    invoice.DueDate = updateInvoiceDto.DueDate.Value;
                
                if (updateInvoiceDto.BillingPeriodStart.HasValue)
                    invoice.BillingPeriodStart = updateInvoiceDto.BillingPeriodStart.Value;
                
                if (updateInvoiceDto.BillingPeriodEnd.HasValue)
                    invoice.BillingPeriodEnd = updateInvoiceDto.BillingPeriodEnd.Value;
                
                if (!string.IsNullOrEmpty(updateInvoiceDto.Status))
                    invoice.Status = updateInvoiceDto.Status;
                
                if (!string.IsNullOrEmpty(updateInvoiceDto.Notes))
                    invoice.Notes = updateInvoiceDto.Notes;

                invoice.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to update invoice", error = ex.Message });
            }
        }

        // POST: api/Invoices/5/payment
        [HttpPost("{id}/payment")]
        public async Task<IActionResult> RecordPayment(int id, RecordPaymentDto paymentDto)
        {
            try
            {
                var invoice = await _context.Invoices.FindAsync(id);
                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found" });
                }

                // Update paid amount
                invoice.PaidAmount += paymentDto.Amount;
                
                // Update status based on payment
                if (invoice.PaidAmount >= invoice.TotalAmount)
                {
                    invoice.Status = "paid";
                }
                else if (invoice.PaidAmount > 0)
                {
                    invoice.Status = "partial";
                }

                invoice.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Payment recorded successfully", paidAmount = invoice.PaidAmount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to record payment", error = ex.Message });
            }
        }

        // GET: api/Invoices/customer/5
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByCustomer(int customerId)
        {
            try
            {
                var invoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.CreatedBy)
                    .Include(i => i.Items)
                    .Where(i => i.ContractCustomerId == customerId)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToListAsync();

                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve customer invoices", error = ex.Message });
            }
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.Items)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found" });
                }

                // Only allow deletion if status is draft
                if (invoice.Status != "draft")
                {
                    return BadRequest(new { message = "Only draft invoices can be deleted" });
                }

                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to delete invoice", error = ex.Message });
            }
        }

        // POST: api/Invoices/5/send
        [HttpPost("{id}/send")]
        public async Task<IActionResult> SendInvoice(int id)
        {
            try
            {
                var invoice = await _context.Invoices.FindAsync(id);
                if (invoice == null)
                {
                    return NotFound(new { message = "Invoice not found" });
                }

                if (invoice.Status != "draft")
                {
                    return BadRequest(new { message = "Only draft invoices can be sent" });
                }

                invoice.Status = "sent";
                invoice.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Invoice sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to send invoice", error = ex.Message });
            }
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;
            
            // Count invoices this month
            var monthlyCount = await _context.Invoices
                .Where(i => i.CreatedAt.Year == currentYear && i.CreatedAt.Month == currentMonth)
                .CountAsync();

            var invoiceNumber = $"INV-{currentYear:D4}{currentMonth:D2}-{(monthlyCount + 1):D4}";
            
            // Ensure uniqueness
            while (await _context.Invoices.AnyAsync(i => i.InvoiceNumber == invoiceNumber))
            {
                monthlyCount++;
                invoiceNumber = $"INV-{currentYear:D4}{currentMonth:D2}-{(monthlyCount + 1):D4}";
            }

            return invoiceNumber;
        }
    }
}