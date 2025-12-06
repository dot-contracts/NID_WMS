const express = require('express');
const router = express.Router();
const jwt = require('jsonwebtoken');
const axios = require('axios');

// Login route
router.post('/login', async (req, res) => {
    try {
      const { username, password } = req.body;
  
      const response = await axios.post(`${process.env.API_URL}/api/auth/login`, {
        username,
        password
      });
  
      const { Token, Username, Role, UserId, Branch } = response.data;
  
      if (!['admin', 'branch_manager'].includes(Role)) {
        return res.status(403).json({ message: 'Access denied: insufficient permissions' });
      }
  
      // Return .NET API's token directly
      res.json({
        token: Token,
        user: {
          id: UserId,
          username: Username,
          role: Role,
          branch: Branch?.Name
        }
      });
    } catch (error) {
      const statusCode = error.response?.status || 500;
      const message = error.response?.data?.message || 'Authentication failed';
      res.status(statusCode).json({ message });
    }
  });

module.exports = router; 