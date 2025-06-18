import { CategoryDto } from '../category/category-dto.schema';

export interface ProductDto {
  id: string;
  productName: string;
  description?: string;
  price: number;
  discountPrice?: number;
  stockQuantity: number;
  imageUrl?: string;
  imageUrls?: string; // Assuming this is a comma-separated string or JSON string of URLs
  isActive: boolean;
  isFeatured: boolean;
  createdAt: Date;
  updatedAt?: Date;
  categoryId: string;
  category?: CategoryDto;
}
