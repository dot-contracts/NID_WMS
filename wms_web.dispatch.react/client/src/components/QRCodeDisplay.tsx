import React, { useState, useEffect } from 'react';
import { generateQRCode } from '../utils/qrCode';

interface QRCodeDisplayProps {
  data: string;
  size?: number;
  className?: string;
  alt?: string;
  onError?: (error: Error) => void;
}

export const QRCodeDisplay: React.FC<QRCodeDisplayProps> = ({
  data,
  size = 100,
  className = '',
  alt = 'QR Code',
  onError
}) => {
  const [qrCodeUrl, setQrCodeUrl] = useState<string>('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    const generateQR = async () => {
      if (!data) {
        setError('No data provided for QR code');
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        setError('');
        
        const qrUrl = await generateQRCode(data, {
          width: size * 2, // Higher resolution for better quality
          margin: 1
        });
        
        setQrCodeUrl(qrUrl);
      } catch (err) {
        const errorMessage = err instanceof Error ? err.message : 'Failed to generate QR code';
        setError(errorMessage);
        onError?.(err instanceof Error ? err : new Error(errorMessage));
      } finally {
        setLoading(false);
      }
    };

    generateQR();
  }, [data, size, onError]);

  if (!data) {
    return (
      <div 
        className={`flex items-center justify-center border border-gray-300 bg-gray-100 text-gray-500 text-xs ${className}`}
        style={{ width: size, height: size }}
      >
        No Data
      </div>
    );
  }

  if (loading) {
    return (
      <div 
        className={`flex items-center justify-center border border-gray-300 bg-gray-100 text-gray-500 text-xs ${className}`}
        style={{ width: size, height: size }}
      >
        Loading...
      </div>
    );
  }

  if (error) {
    return (
      <div 
        className={`flex items-center justify-center border border-red-300 bg-red-100 text-red-500 text-xs ${className}`}
        style={{ width: size, height: size }}
      >
        QR Error
      </div>
    );
  }

  return (
    <img
      src={qrCodeUrl}
      alt={alt}
      className={className}
      style={{ width: size, height: size }}
    />
  );
};