import Image from 'next/image';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardFooter } from '@/components/ui/card';
import type { ProductDto } from '@/lib/schemas/product/product-dto.schema';
import Link from 'next/link';

interface ProductCardProps {
  product: ProductDto;
}

export function ProductCard({ product }: ProductCardProps) {
  const formattedPrice = new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND',
  }).format(product.price);
  const formattedDiscountPrice = product.discountPrice
    ? new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
      }).format(product.discountPrice)
    : null;

  return (
    <Card className='flex flex-col overflow-hidden rounded-lg shadow-lg transition-all hover:shadow-xl'>
      <Link
        href={`/products/${product.id}`}
        className='relative block h-48 w-full overflow-hidden'
      >
        <Image
          src={product.imageUrl || '/placeholder.svg?height=400&width=300'}
          alt={product.productName}
          fill
          className='object-cover transition-transform duration-300 hover:scale-105'
        />
      </Link>
      <CardContent className='flex-1 p-4'>
        <h3 className='text-lg font-semibold text-flora-text line-clamp-2'>
          <Link href={`/products/${product.id}`} className='hover:underline'>
            {product.productName}
          </Link>
        </h3>
        {product.category && (
          <p className='text-sm text-muted-foreground mt-1'>
            Danh mục: {product.category.categoryName}
          </p>
        )}
        <div className='mt-2 flex items-baseline gap-2'>
          {formattedDiscountPrice ? (
            <>
              <span className='text-xl font-bold text-flora-primary'>
                {formattedDiscountPrice}
              </span>
              <span className='text-sm text-muted-foreground line-through'>
                {formattedPrice}
              </span>
            </>
          ) : (
            <span className='text-xl font-bold text-flora-primary'>
              {formattedPrice}
            </span>
          )}
        </div>
        {product.stockQuantity <= 0 && (
          <p className='text-sm text-destructive mt-1'>Hết hàng</p>
        )}
      </CardContent>
      <CardFooter className='p-4 pt-0'>
        <Button
          className='w-full bg-flora-primary hover:bg-flora-primary/90'
          disabled={product.stockQuantity <= 0}
        >
          Thêm vào giỏ
        </Button>
      </CardFooter>
    </Card>
  );
}
