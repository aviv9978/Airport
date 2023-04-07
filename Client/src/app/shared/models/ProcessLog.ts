import { Flight } from './Flight';

export interface ProcessLog {
  id: number;
  legNum: Number;
  flight: Flight;
  enterTime: Date;
  exitTime: Date;
  message: string;
}
