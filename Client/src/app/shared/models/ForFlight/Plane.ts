import { Company } from './Company';

export interface Plane {
  company: Company;
  model: string;
  passCounter: number;
}
