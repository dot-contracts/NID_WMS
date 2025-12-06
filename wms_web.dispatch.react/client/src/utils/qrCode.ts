import QRCode from 'qrcode';

/**
 * Generate a QR code as a data URL (base64 image)
 * @param text - The text to encode in the QR code
 * @param options - QR code generation options
 * @returns Promise<string> - Data URL of the QR code image
 */
export const generateQRCode = async (
  text: string, 
  options: {
    width?: number;
    margin?: number;
    color?: {
      dark?: string;
      light?: string;
    };
  } = {}
): Promise<string> => {
  try {
    const qrCodeOptions = {
      width: options.width || 200,
      margin: options.margin || 2,
      color: {
        dark: options.color?.dark || '#000000',
        light: options.color?.light || '#FFFFFF'
      }
    };

    const dataUrl = await QRCode.toDataURL(text, qrCodeOptions);
    return dataUrl;
  } catch (error) {
    console.error('Error generating QR code:', error);
    throw new Error('Failed to generate QR code');
  }
};

/**
 * Generate a QR code as an SVG string
 * @param text - The text to encode in the QR code
 * @param options - QR code generation options
 * @returns Promise<string> - SVG string of the QR code
 */
export const generateQRCodeSVG = async (
  text: string,
  options: {
    width?: number;
    margin?: number;
    color?: {
      dark?: string;
      light?: string;
    };
  } = {}
): Promise<string> => {
  try {
    const qrCodeOptions = {
      width: options.width || 200,
      margin: options.margin || 2,
      color: {
        dark: options.color?.dark || '#000000',
        light: options.color?.light || '#FFFFFF'
      }
    };

    const svgString = await QRCode.toString(text, { 
      type: 'svg',
      ...qrCodeOptions
    });
    return svgString;
  } catch (error) {
    console.error('Error generating QR code SVG:', error);
    throw new Error('Failed to generate QR code SVG');
  }
};