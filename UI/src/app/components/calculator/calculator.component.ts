import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  CalculationDto,
  CalculationResultDto,
} from '../../models/calculation-dto.model';
import { CalculationService } from '../../services/calculation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-calculator',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.css',
})
export class CalculatorComponent implements OnInit {
  data: CalculationDto = {
    value1Calculation: '',
    value2Calculation: '',
    typeCalculation: '',
  };

  resultData: CalculationResultDto = {
    result: '',
    last3SameType: [],
    sameTypeCountThisMonth: 0,
  };
  operations: string[] = [];

  constructor(private calcService: CalculationService) {}

  ngOnInit(): void {
    this.calcService
      .getOperations()
      .subscribe((ops) => (this.operations = ops));
  }

  calculate(): void {
    if (
      this.data.value1Calculation == '' ||
      this.data.value2Calculation == ''
    ) {
      alert('נא למלא את כל השדות');
      return;
    }

    if (
      this.data.value1Calculation.length > 50 ||
      this.data.value2Calculation.length > 50
    ) {
      alert('נא להכניס עד 50 תווים');
      return;
    }

    this.calcService.calculate(this.data).subscribe((result) => {
      this.resultData = result;
    });
  }
}
