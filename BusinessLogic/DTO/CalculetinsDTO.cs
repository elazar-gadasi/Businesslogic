using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO
{
    public class CalculetinsDTO
    {
        [MaxLength(50, ErrorMessage = "לא ניתן להכניס יותר מ-50 תווים בערך השני.")]
        public string Value1Calculation { get; set; }
        [MaxLength(50, ErrorMessage = "לא ניתן להכניס יותר מ-50 תווים בערך השני.")]
        public string Value2Calculation { get; set; }

        public string TypeCalculation { get; set; }

    }

    public class CalculationResultDTO
    {
        public string Result { get; set; }
        public List<Calculation> Last3SameType { get; set; } = new List<Calculation>();
        public int SameTypeCountThisMonth { get; set; }
    }

}
