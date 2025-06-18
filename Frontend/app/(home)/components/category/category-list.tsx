'use client';

import { useEffect, useState } from 'react';
import type { CategoryDto } from '@/lib/schemas/category/category-dto.schema';
import { CategoryCard } from './category-card';
import { getActiveCategories } from '@/services/categoryService';

export function CategoryList() {
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        setLoading(true);
        const fetchedCategories = await getActiveCategories();
        setCategories(fetchedCategories);
      } catch (err) {
        setError('Cannot load category.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  if (loading) {
    return (
      <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-6'>
        {Array.from({ length: 6 }).map((_, i) => (
          <div
            key={i}
            className='h-48 w-full animate-pulse rounded-lg bg-muted'
          />
        ))}
      </div>
    );
  }

  if (error) {
    return <div className='text-center text-destructive'>{error}</div>;
  }

  if (categories.length === 0) {
    return (
      <div className='text-center text-muted-foreground'>
        Không có danh mục nào.
      </div>
    );
  }

  return (
    <section className='py-12 bg-flora-background-light'>
      <div className='container'>
        <h2 className='text-3xl font-bold text-center mb-8 text-flora-text'>
          Khám phá danh mục
        </h2>
        <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-6'>
          {categories.map((category) => (
            <CategoryCard key={category.id} category={category} />
          ))}
        </div>
      </div>
    </section>
  );
}
