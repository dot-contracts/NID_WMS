const express = require('express');
const router = express.Router();
const { auth, checkRole } = require('../middleware/auth');
const axios = require('axios');

// Get parcels based on user role and branch
router.get('/', auth, async (req, res) => {
  try {
    const { role, branch } = req.user;
    let url = `${process.env.API_URL}/api/parcels`;

    // If branch manager, only get parcels for their branch
    if (role === 'branch_manager') {
      // First get pending parcels
      const pendingResponse = await axios.get(`${url}/pending`, {
        headers: { Authorization: `Bearer ${req.header('Authorization')}` }
      });

      // Filter parcels by branch
      const parcels = pendingResponse.data.filter(parcel => 
        parcel.destination.toLowerCase() === branch.toLowerCase()
      );

      return res.json(parcels);
    }

    // For admin, get all parcels
    const response = await axios.get(url, {
      headers: { Authorization: `Bearer ${req.header('Authorization')}` }
    });

    res.json(response.data);
  } catch (error) {
    console.error('Error fetching parcels:', error.response?.data || error.message);
    res.status(500).json({ message: 'Error fetching parcels' });
  }
});

// Get parcel by waybill number
router.get('/waybill/:waybillNumber', auth, async (req, res) => {
  try {
    const { waybillNumber } = req.params;
    const response = await axios.get(
      `${process.env.API_URL}/api/parcels/waybill/${waybillNumber}`,
      {
        headers: { Authorization: `Bearer ${req.header('Authorization')}` }
      }
    );

    res.json(response.data);
  } catch (error) {
    console.error('Error fetching parcel:', error.response?.data || error.message);
    res.status(500).json({ message: 'Error fetching parcel' });
  }
});

// Update parcel status (admin only)
router.put('/:id/status', auth, checkRole(['admin']), async (req, res) => {
  try {
    const { id } = req.params;
    const { status } = req.body;

    const response = await axios.put(
      `${process.env.API_URL}/api/parcels/${id}/status`,
      { status },
      {
        headers: { Authorization: `Bearer ${req.header('Authorization')}` }
      }
    );

    res.json(response.data);
  } catch (error) {
    console.error('Error updating parcel status:', error.response?.data || error.message);
    res.status(500).json({ message: 'Error updating parcel status' });
  }
});

// Get parcels by user
router.get('/user/:userId', auth, async (req, res) => {
  try {
    const { userId } = req.params;
    const response = await axios.get(
      `${process.env.API_URL}/api/parcels/user/${userId}`,
      {
        headers: { Authorization: `Bearer ${req.header('Authorization')}` }
      }
    );

    res.json(response.data);
  } catch (error) {
    console.error('Error fetching user parcels:', error.response?.data || error.message);
    res.status(500).json({ message: 'Error fetching user parcels' });
  }
});

module.exports = router; 