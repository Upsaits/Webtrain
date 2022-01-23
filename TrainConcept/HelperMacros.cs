using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept
{
    /// <summary>
    /// Stellt einige C++-Makros bereit.
    /// </summary>
    static class HelperMacros
    {
        // BYTE = Byte
        // WORD = Int16
        // DWORD = LONG = Int32
        // QWORD = Int64

        /// <summary>
        /// MAKEWORD Makro: http://msdn.microsoft.com/de-de/library/windows/desktop/ms632663.aspx
        /// Verknüpft 2 Byte-Werte zu einem UInt16-Wert.
        /// </summary>
        /// <param name="a">Der erste Byte-Wert.</param>
        /// <param name="b">Der zweite Byte-Wert.</param>
        /// <returns>Die Verknüpfung der 2 Byte-Werte.</returns>
        public static UInt16 MAKEWORD(Byte a, Byte b)
        {
            return (UInt16)(a << 8 | b);
        }

        /// <summary>
        /// MAKELONG Makro: http://msdn.microsoft.com/de-de/library/windows/desktop/ms632660.aspx
        /// Verknüpft 2 UInt16-Werte zu einem UInt32-Wert.
        /// </summary>
        /// <param name="a">Der erste UInt16-Wert.</param>
        /// <param name="b">Der zweite UInt16-Wert.</param>
        /// <returns>Die Verknüpfung der 2 UInt16-Werte.</returns>
        public static UInt32 MAKELONG(UInt16 a, UInt16 b)
        {
            return (UInt32)(a << 16 | b);
        }

        /// <summary>
        /// LOBYTE-Makro: http://msdn.microsoft.com/de-de/library/windows/desktop/ms632658.aspx
        /// Gibt den niederen Byte-Wert des UInt16-Werts zurück.
        /// </summary>
        /// <param name="a">Der UInt16-Wert.</param>
        /// <returns>Der niedere Byte-Wert des UInt16-Wertes.</returns>
        public static Byte LOBYTE(UInt16 a)
        {
            return (Byte)(a & 0xff);
        }

        /// <summary>
        /// HIBYTE-Makro: http://msdn.microsoft.com/de-de/library/windows/desktop/ms632656.aspx
        /// Gibt den höheren Byte-Wert des UInt16-Werts zurück.
        /// </summary>
        /// <param name="a">Der UInt16-Wert.</param>
        /// <returns>Der höhere Byte-Wert des UInt16-Wertes.</returns>
        public static Byte HIBYTE(UInt16 a)
        {
            return (Byte)(a >> 8);
        }

        /// <summary>
        /// LOWORD-Makro: http://msdn.microsoft.com/de-de/library/windows/desktop/ms632659.aspx
        /// Gibt den niederen UInt16-Wert des UInt32-Werts zurück.
        /// </summary>
        /// <param name="a">Der UInt32-Wert.</param>
        /// <returns>Der niedere UInt16-Wert des UInt32-Wertes.</returns>
        public static UInt16 LOWORD(UInt32 a)
        {
            return (UInt16)(a & 0xffff);
        }

        /// <summary>
        /// HIWORD-Makro: http://msdn.microsoft.com/de-de/library/windows/desktop/ms632657.aspx
        /// Gibt den höheren UInt16-Wert des UInt32-Werts zurück.
        /// </summary>
        /// <param name="a">Der UInt32-Wert.</param>
        /// <returns>Der höhere UInt16-Wert des UInt32-Wertes.</returns>
        public static UInt16 HIWORD(UInt32 a)
        {
            return (UInt16)(a >> 16);
        }
    }
}
