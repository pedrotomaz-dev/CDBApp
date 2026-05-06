import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppComponent } from './app.component';
import { CdbService } from './cdb.service';
import { of, throwError } from 'rxjs';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let cdbServiceSpy: jasmine.SpyObj<CdbService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('CdbService', ['calculate']);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, HttpClientTestingModule],
      declarations: [AppComponent],
      providers: [
        { provide: CdbService, useValue: spy }
      ]
    }).compileComponents();

    cdbServiceSpy = TestBed.inject(CdbService) as jasmine.SpyObj<CdbService>;
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should invalidate form when empty', () => {
    expect(component.form.valid).toBeFalsy();
  });

  it('should validate form when correct data is provided', () => {
    component.form.controls['initialValue'].setValue(1000);
    component.form.controls['months'].setValue(12);
    expect(component.form.valid).toBeTruthy();
  });

  it('should call service and set result on valid submit', () => {
    const mockResult = { grossValue: 1100, netValue: 1080 };
    cdbServiceSpy.calculate.and.returnValue(of(mockResult));

    component.form.controls['initialValue'].setValue(1000);
    component.form.controls['months'].setValue(12);
    component.onSubmit();

    expect(cdbServiceSpy.calculate).toHaveBeenCalledWith({ initialValue: 1000, months: 12 });
    expect(component.result).toEqual(mockResult);
    expect(component.errorMessage).toBe('');
  });

  it('should set error message when service fails', () => {
    cdbServiceSpy.calculate.and.returnValue(throwError(() => ({ error: { message: 'Erro de teste' } })));

    component.form.controls['initialValue'].setValue(1000);
    component.form.controls['months'].setValue(12);
    component.onSubmit();

    expect(component.result).toBeNull();
    expect(component.errorMessage).toBe('Erro de teste');
  });
});
