import { Flight } from './Flight';
import { LegNumber } from './enums/legNumbers';
export interface LegStatus {
  legNumber: LegNumber;
  isOccupied: boolean;
  flight?: Flight;
}
