using BusinessLogic.Data;
using BusinessLogic.DTO;
using BusinessLogic.Models;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Services
{
    public interface ICalculetionsService
    {
        List<string> GetOperations();
        CalculationResultDTO CalculetinResult(CalculetinsDTO obj);
    }

    public class CalculetionsService : ICalculetionsService
    {
        private ApplicationDbContext _context;

        public CalculetionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public CalculationResultDTO CalculetinResult(CalculetinsDTO obj)
        {
            if (string.IsNullOrEmpty(obj.Value1Calculation) || string.IsNullOrEmpty(obj.Value2Calculation))
            {
                return new CalculationResultDTO { Result = "שגיאה: אחד מהשדות ריק.",Last3SameType = [], SameTypeCountThisMonth = 0 };
            }

            if (string.IsNullOrEmpty(obj.TypeCalculation))
            {
                return new CalculationResultDTO { Result = "שגיאה: לא נבחרה פעולה.", Last3SameType = [], SameTypeCountThisMonth = 0 };
            }


            bool isNumA = double.TryParse(obj.Value1Calculation, out var numA);
            bool isNumB = double.TryParse(obj.Value2Calculation, out var numB);
            string result = string.Empty;

            switch (obj.TypeCalculation)
            {
                case "חיבור":
                    result = (isNumA && isNumB) ? (numA + numB).ToString() : obj.Value1Calculation + obj.Value2Calculation;
                    break;

                case "חיסור":
                    result = (isNumA && isNumB) ? (numA - numB).ToString()
                            : $"לא ניתן לבצע חיסור על מחרוזות: \"{obj.Value1Calculation}\" , \"{obj.Value2Calculation}\"";
                    break;

                case "כפל":
                    if (isNumA && isNumB)
                        result = (numA * numB).ToString();
                    else if (!isNumA && isNumB)
                        result = string.Concat(Enumerable.Repeat(obj.Value1Calculation, (int)numB));
                    else
                        result = $"לא ניתן להכפיל את הערכים האלו.";
                    break;

                case "חילוק":
                    if (isNumA && isNumB)
                        result = (numB == 0) ? "שגיאה: חילוק באפס." : (numA / numB).ToString();
                    else
                        result = $"לא ניתן לבצע חילוק על מחרוזות.";
                    break;

                case "שרשור":
                    result = obj.Value1Calculation + obj.Value2Calculation;
                    break;

                case "שווה":
                    result = (obj.Value1Calculation == obj.Value2Calculation).ToString();
                    break;

                case "מקסימום":
                    result = (isNumA && isNumB) ? Math.Max(numA, numB).ToString()
                            : (string.Compare(obj.Value1Calculation, obj.Value2Calculation) > 0 ? obj.Value1Calculation : obj.Value2Calculation);
                    break;

                case "מינימום":
                    result = (isNumA && isNumB) ? Math.Min(numA, numB).ToString()
                            : (string.Compare(obj.Value1Calculation, obj.Value2Calculation) < 0 ? obj.Value1Calculation : obj.Value2Calculation);
                    break;

                default:
                    return new CalculationResultDTO { Result = $"שגיאה: פעולה לא נתמכת: {obj.TypeCalculation}" };
            }

            var calculation = new Calculation
            {
                Value1Calculation = obj.Value1Calculation,
                Value2Calculation = obj.Value2Calculation,
                TypeCalculation = obj.TypeCalculation,
                ResCalculation = result,
                CreatDate = DateTime.Now
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            // קבלת 3 פעולות אחרונות מאותו סוג
            var last3 = _context.Calculations
                .Where(c => c.TypeCalculation == obj.TypeCalculation)
                .OrderByDescending(c => c.CreatDate)
                .Take(3)
                .ToList();

            // ספירת פעולות מאותו סוג החודש
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var countThisMonth = _context.Calculations
                .Where(c => c.TypeCalculation == obj.TypeCalculation && c.CreatDate >= monthStart)
                .Count();

            return new CalculationResultDTO
            {
                Result = result,
                Last3SameType = last3,
                SameTypeCountThisMonth = countThisMonth
            };
        }

        public List<string> GetOperations()
        {
            return new List<string>
            {
                "חיבור",
                "חיסור",
                "כפל",
                "חילוק",
                "שרשור",
                "שווה",
                "מקסימום",
                "מינימום"
            };
        }



    }
}
