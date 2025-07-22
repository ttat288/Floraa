import { http } from '@/lib/common/http.type';
import type { ProductDto } from '@/lib/schemas/product/product-dto.schema';

export const getFeaturedProducts = async (): Promise<ProductDto[]> => {
  try {
    const response = await http.get<ProductDto[]>(`/api/Products/active`, {
      next: { tags: ['product-feature'] },
    });

    return response;
  } catch (error) {
    const serviceError = error as Error;
    console.error('Error fetching products:', serviceError);
    throw error;
  }
};
