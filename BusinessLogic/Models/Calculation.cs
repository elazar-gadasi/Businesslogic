using System;
using System.Collections.Generic;

namespace BusinessLogic.Models;

public partial class Calculation
{
    public int Id { get; set; }

    public string Value1Calculation { get; set; } = null!;

    public string Value2Calculation { get; set; } = null!;

    public string TypeCalculation { get; set; } = null!;

    public string ResCalculation { get; set; } = null!;

    public DateTime CreatDate { get; set; }
}
