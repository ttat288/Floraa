import { ApiResponse } from '@/lib/common/BaseResponse';
import { http } from '@/lib/common/http.type';
import { CategoryDto } from '@/lib/schemas/category/category-dto.schema';

export const getActiveCategories = async (): Promise<CategoryDto[]> => {
  try {
    const response = await http.get<CategoryDto[]>(`/api/Categories/active`, {
      next: { tags: ['category-active'] },
    });

    return response;
  } catch (error: any) {
    console.error('Error fetching cart:', error);
    throw error;
  }
};
