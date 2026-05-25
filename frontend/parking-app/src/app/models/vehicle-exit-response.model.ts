export interface VehicleExitResponse {
  plate: string;
  vehicleType: string;
  entryTime: string;
  exitTime: string;
  totalMinutes: number;
  totalAmount: number;
}