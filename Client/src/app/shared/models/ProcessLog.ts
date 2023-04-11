import { Flight } from './Flight';

export interface ProcessLog {
  id: number;
  legNum: number;
  flight: Flight;
  enterTime: Date;
  exitTime: Date;
  message: string;
}
