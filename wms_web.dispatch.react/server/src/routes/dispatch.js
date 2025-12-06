const express = require('express');
const router = express.Router();
const { auth, checkRole } = require('../middleware/auth');
const axios = require('axios');

// Create new dispatch
router.post('/', auth, checkRole(['branch_manager']), async (req, res) => {
  try {
    const { parcelIds, vehicleNumber, driver } = req.body;
    const { branch } = req.user;

    // Update all parcels' status in a single batch request via Web API
    await axios.put(
      `${process.env.API_URL}/api/parcels/batch/status`,
      { parcelIds, status: 'in_transit' },
      { headers: { Authorization: `Bearer ${req.header('Authorization')}` } }
    );

    // Create dispatch record via Web API
    const dispatchResponse = await axios.post(
      `${process.env.API_URL}/api/dispatches`,
      {
        sourceBranch: branch,
        vehicleNumber,
        driver,
        parcelIds,
        status: 'in_transit'
      },
      { headers: { Authorization: `Bearer ${req.header('Authorization')}` } }
    );

    res.json(dispatchResponse.data);
  } catch (error) {
    console.error('Error creating dispatch:', error.response?.data || error.message);
    res.status(500).json({ message: 'Error creating dispatch' });
  }
});

// Generate dispatch note
router.get('/note/:dispatchId', auth, async (req, res) => {
  try {
    const { dispatchId } = req.params;
    const response = await axios.get(
      `${process.env.API_URL}/api/dispatch/${dispatchId}/note`,
      {
        headers: { Authorization: `Bearer ${req.header('Authorization')}` }
      }
    );

    res.json(response.data);
  } catch (error) {
    console.error('Error generating dispatch note:', error);
    res.status(500).json({ message: 'Error generating dispatch note' });
  }
});

// Get dispatches for branch
router.get('/branch', auth, checkRole(['branch_manager']), async (req, res) => {
  try {
    const { branch } = req.user;

    // Get all parcels with 'in_transit' status
    const response = await axios.get(
      `${process.env.API_URL}/api/parcels`,
      {
        headers: { Authorization: `Bearer ${req.header('Authorization')}` }
      }
    );

    // Filter parcels by branch and status
    const dispatches = response.data.filter(parcel => 
      parcel.destination.toLowerCase() === branch.toLowerCase() &&
      parcel.status === 'in_transit'
    );

    res.json(dispatches);
  } catch (error) {
    console.error('Error fetching dispatches:', error.response?.data || error.message);
    res.status(500).json({ message: 'Error fetching dispatches' });
  }
});

module.exports = router; 