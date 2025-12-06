using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wms_android.Interfaces
{
    /// <summary>
    /// Interface for POS printer functionality
    /// </summary>
    public interface IPosApiHelper
    {
        /// <summary>
        /// Initialize the printer
        /// </summary>
        int PrintInit(int gray, int density, int mode, int otherOptions);

        /// <summary>
        /// Set the alignment for text printing
        /// 0 = Left, 1 = Center, 2 = Right
        /// </summary>
        int PrintSetAlign(int align);

        /// <summary>
        /// Set the font for text printing
        /// </summary>
        int PrintSetFont(sbyte width, sbyte height, sbyte style);

        /// <summary>
        /// Print text
        /// </summary>
        int PrintStr(string text);

        /// <summary>
        /// Print a QR code
        /// </summary>
        int PrintQrCode_Cut(string content, int width, int height, string format);

        /// <summary>
        /// Feed paper by a number of lines
        /// </summary>
        int PrintFeedPaper(int lines);

        /// <summary>
        /// Start the printing process
        /// </summary>
        int PrintStart();
        
        /// <summary>
        /// Checks if paper is loaded and ready for printing
        /// </summary>
        /// <returns>True if paper is ready, false otherwise</returns>
        bool IsPaperReady();
        
        /// <summary>
        /// Gets a human-readable status message about the printer
        /// </summary>
        /// <returns>Status message</returns>
        string GetStatusMessage();
        
        /// <summary>
        /// Sets the default font settings appropriate for the device
        /// </summary>
        /// <returns>0 if successful, -1 if there was an error</returns>
        int SetDefaultFont();
    }
}
