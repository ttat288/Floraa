'use client';

import { useEffect, useState } from 'react';
import type { ProductDto } from '@/lib/schemas/product/product-dto.schema';
import { ProductCard } from './product-card';
import { getFeaturedProducts } from '@/services/productService';

export function FeaturedProducts() {
  const [featuredProducts, setFeaturedProducts] = useState<ProductDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchFeaturedProducts = async () => {
      try {
        setLoading(true);
        const products = await getFeaturedProducts();
        setFeaturedProducts(products);
      } catch (err) {
        setError('Không thể tải sản phẩm nổi bật.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchFeaturedProducts();
  }, []);

  if (loading) {
    return (
      <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6'>
        {Array.from({ length: 4 }).map((_, i) => (
          <div
            key={i}
            className='h-80 w-full animate-pulse rounded-lg bg-muted'
          />
        ))}
      </div>
    );
  }

  if (error) {
    return <div className='text-center text-destructive'>{error}</div>;
  }

  if (featuredProducts.length === 0) {
    return (
      <div className='text-center text-muted-foreground'>
        Không có sản phẩm nổi bật nào.
      </div>
    );
  }

  return (
    <section className='py-12'>
      <div className='container'>
        <h2 className='text-3xl font-bold text-center mb-8 text-flora-text'>
          Sản phẩm nổi bật
        </h2>
        <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6'>
          {featuredProducts.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      </div>
    </section>
  );
}
