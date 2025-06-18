export interface CategoryDto {
  id: string;
  categoryName: string;
  description?: string;
  imageUrl?: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt?: Date;
}
