import { http } from '@/lib/common/http.type';
import type { CategoryDto } from '@/lib/schemas/category/category-dto.schema';

export const getActiveCategories = async (): Promise<CategoryDto[]> => {
  try {
    const response = await http.get<CategoryDto[]>(`/api/Categories/active`, {
      next: { tags: ['category-active'] },
    });

    return response;
  } catch (error) {
    const serviceError = error as Error;
    console.error('Error fetching categories:', serviceError);
    throw error;
  }
};
