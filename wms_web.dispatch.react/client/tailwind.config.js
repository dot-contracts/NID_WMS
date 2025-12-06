/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        outfit: ['Outfit', 'sans-serif'],
      },
      fontSize: {
        'theme-xl': ['20px', '30px'],
        'theme-sm': ['14px', '20px'],
        'theme-xs': ['12px', '18px'],
        'title-2xl': ['72px', '90px'],
        'title-xl': ['60px', '72px'],
        'title-lg': ['48px', '60px'],
        'title-md': ['36px', '44px'],
        'title-sm': ['30px', '38px'],
      },
      colors: {
        // Brand colors using exact Django green theme
        brand: {
          25: '#f0fdf4',
          50: '#d8f3dc', // Background mint from Django
          100: '#b7e4c7',
          200: '#95d5b2',
          300: '#74c69d', // Primary light from Django
          400: '#52a276',
          500: '#40916c', // Exact primary green from Django
          600: '#347a5c',
          700: '#1b4332', // Exact primary dark from Django
          800: '#133026',
          900: '#0f261c',
          950: '#081914',
        },
        // Primary alias for compatibility with existing code
        primary: {
          25: '#f0fdf4',
          50: '#d8f3dc', // Background mint from Django
          100: '#b7e4c7',
          200: '#95d5b2',
          300: '#74c69d', // Primary light from Django
          400: '#52a276',
          500: '#40916c', // Exact primary green from Django
          600: '#347a5c',
          700: '#1b4332', // Exact primary dark from Django
          800: '#133026',
          900: '#0f261c',
          950: '#081914',
        },
        gray: {
          25: '#fcfcfd',
          50: '#f9fafb',
          100: '#f2f4f7',
          200: '#e4e7ec',
          300: '#d0d5dd',
          400: '#98a2b3',
          500: '#667085',
          600: '#475467',
          700: '#344054',
          800: '#1d2939',
          900: '#101828',
          950: '#0c111d',
        },
        success: {
          25: '#f6fef9',
          50: '#ecfdf3',
          100: '#d1fadf',
          200: '#a6f4c5',
          300: '#6ce9a6',
          400: '#32d583',
          500: '#12b76a',
          600: '#039855',
          700: '#027a48',
          800: '#05603a',
          900: '#054f31',
          950: '#053321',
        },
        error: {
          25: '#fffbfa',
          50: '#fef3f2',
          100: '#fee4e2',
          200: '#fecdca',
          300: '#fda29b',
          400: '#f97066',
          500: '#f04438',
          600: '#d92d20',
          700: '#b42318',
          800: '#912018',
          900: '#7a271a',
          950: '#55160c',
        },
        warning: {
          25: '#fffcf5',
          50: '#fffaeb',
          100: '#fef0c7',
          200: '#fedf89',
          300: '#fec84b',
          400: '#fdb022',
          500: '#f79009',
          600: '#dc6803',
          700: '#b54708',
          800: '#93370d',
          900: '#7a2e0e',
          950: '#4e1d09',
        },
      },
      boxShadow: {
        'theme-xs': '0px 1px 2px 0px rgba(16, 24, 40, 0.05)',
        'theme-sm': '0px 1px 3px 0px rgba(16, 24, 40, 0.1), 0px 1px 2px 0px rgba(16, 24, 40, 0.06)',
        'theme-md': '0px 4px 8px -2px rgba(16, 24, 40, 0.1), 0px 2px 4px -2px rgba(16, 24, 40, 0.06)',
        'theme-lg': '0px 12px 16px -4px rgba(16, 24, 40, 0.08), 0px 4px 6px -2px rgba(16, 24, 40, 0.03)',
        'theme-xl': '0px 20px 24px -4px rgba(16, 24, 40, 0.08), 0px 8px 8px -4px rgba(16, 24, 40, 0.03)',
      },
      zIndex: {
        '1': '1',
        '9': '9',
        '99': '99',
        '999': '999',
        '9999': '9999',
        '99999': '99999',
        '999999': '999999',
      },
    },
  },
  plugins: [],
} 