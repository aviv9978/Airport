import { Flight } from './Flight';

export interface ProcessLog {
  id: number;
  legNumber: string;
  flight: Flight;
  enterTime: Date;
  exitTime: Date;
  message: string;
}
