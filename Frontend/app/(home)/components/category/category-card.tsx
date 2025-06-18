import Image from 'next/image';
import Link from 'next/link';
import { Card, CardContent } from '@/components/ui/card';
import type { CategoryDto } from '@/lib/schemas/category/category-dto.schema';

interface CategoryCardProps {
  category: CategoryDto;
}

export function CategoryCard({ category }: CategoryCardProps) {
  return (
    <Link href={`/products?category=${category.id}`} className='block'>
      <Card className='flex flex-col items-center justify-center overflow-hidden rounded-lg shadow-lg transition-all hover:shadow-xl hover:scale-[1.02]'>
        <div className='relative h-32 w-full'>
          <Image
            src={category.imageUrl || '/placeholder.svg?height=128&width=128'}
            alt={category.categoryName}
            fill
            className='object-cover'
          />
        </div>
        <CardContent className='p-4 text-center'>
          <h3 className='text-lg font-semibold text-flora-text'>
            {category.categoryName}
          </h3>
        </CardContent>
      </Card>
    </Link>
  );
}
