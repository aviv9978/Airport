using System.ComponentModel;

namespace Core.Enums
{
    [Flags]
    public enum LegNumber
    {
        [Description("1")]
        One = 1, 
        [Description("2")]
        Two = 2,
        [Description("3")]
        Thr = 4,
        [Description("4")]
        Fou = 8,
        [Description("5")]
        Fiv = 16,
        [Description("6")]
        Six = 32,
        [Description("7")]
        Sev = 64,
        [Description("8")]
        Eig = 128,
        [Description("Air")]
        Air = 256
    }
}
