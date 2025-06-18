import { ApiResponse } from '@/lib/common/BaseResponse';
import { http } from '@/lib/common/http.type';
import { ProductDto } from '@/lib/schemas/product/product-dto.schema';

export const getFeaturedProducts = async (): Promise<ProductDto[]> => {
  try {
    const response = await http.get<ProductDto[]>(`/api/Products/active`, {
      next: { tags: ['product-feature'] },
    });

    return response;
  } catch (error: any) {
    console.error('Error fetching product:', error);
    throw error;
  }
};
