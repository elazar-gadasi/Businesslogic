export interface CalculationDto {
  value1Calculation: string;
  value2Calculation: string;
  typeCalculation: string;
}

export interface Calculation {
  id: number;
  value1Calculation: string;
  value2Calculation: string;
  typeCalculation: string;
  resCalculation: string;
  creatDate: string;
}

export interface CalculationResultDto {
  result: string;
  last3SameType: Calculation[];
  sameTypeCountThisMonth: number;
}
