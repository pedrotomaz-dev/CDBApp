import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CdbService, CdbCalculationResult } from './cdb.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Cálculo de Investimento CDB';
  form: FormGroup;
  result: CdbCalculationResult | null = null;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private cdbService: CdbService) {
    this.form = this.fb.group({
      initialValue: [null, [Validators.required, Validators.min(0.01)]],
      months: [null, [Validators.required, Validators.min(2)]]
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.errorMessage = '';
    this.result = null;

    this.cdbService.calculate(this.form.value).subscribe({
      next: (res) => {
        this.result = res;
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Ocorreu um erro ao calcular o investimento.';
      }
    });
  }

  reset(): void {
    this.form.reset();
    this.result = null;
    this.errorMessage = '';
  }
}
