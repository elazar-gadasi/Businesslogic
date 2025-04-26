import { Routes } from '@angular/router';
import { CalculatorComponent } from './components/calculator/calculator.component';

export const routes: Routes = [
  { path: '', component: CalculatorComponent },
  { path: 'calculator', component: CalculatorComponent },
  { path: '**', redirectTo: '' },
];
