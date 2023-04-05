import { Flight } from './Flight';

export interface Log {
  LegNum: Number;
  Flight: Flight;
  EnterTime: Date;
  ExitTime: Date;
  Message: string;
}
