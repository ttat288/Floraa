import Image from 'next/image';
import Link from 'next/link';
import { Button } from '@/components/ui/button';

export default function HomePage() {
  return (
    <div className='flex flex-col min-h-[calc(100vh-64px)]'>
      {/* Hero Section */}
      <section className='w-full py-12 md:py-24 lg:py-32'>
        <div className='container grid items-center gap-6 px-4 md:px-6 lg:grid-cols-2 lg:gap-12'>
          {/* Text Content */}
          <div className='flex flex-col justify-center space-y-4 text-center lg:text-left'>
            <h1 className='text-4xl md:text-5xl lg:text-6xl font-bold tracking-tight mb-4 text-flora-text drop-shadow-lg'>
              D08 Floraa - Nơi vẻ đẹp nở hoa
            </h1>
            <p className='text-lg md:text-xl lg:text-2xl mb-8 text-muted-foreground drop-shadow-md'>
              Mang đến những bó hoa tươi đẹp nhất cho mọi khoảnh khắc đặc biệt.
            </p>
            <div className='flex justify-center lg:justify-start'>
              <Button
                size='lg'
                className='bg-flora-primary hover:bg-flora-primary/90 text-flora-primary-foreground shadow-lg transition-transform hover:scale-105'
              >
                <Link href='/products'>Mua sắm ngay</Link>
              </Button>
            </div>
          </div>
          {/* Image */}
          <div className='flex justify-center lg:justify-end'>
            <Image
              src='/roseHeroBackground.png'
              alt='Hero Background'
              width={600} // Adjust width as needed
              height={600} // Adjust height as needed
              className='object-contain'
              priority
            />
          </div>
        </div>
      </section>

      {/* Featured Products Section */}
      {/* <FeaturedProducts /> */}

      {/* Categories Section */}
      {/* <CategoryList /> */}

      {/* Call to Action / About Us Snippet */}
      <section className='py-16 bg-flora-background-light'>
        <div className='container text-center'>
          <h2 className='text-3xl font-bold text-flora-text mb-4'>
            Về D08 Floraa
          </h2>
          <p className='text-lg text-muted-foreground max-w-3xl mx-auto mb-8'>
            Chúng tôi tin rằng mỗi bông hoa đều mang một câu chuyện. Tại D08
            Floraa, chúng tôi tuyển chọn những bông hoa tươi nhất, đẹp nhất để
            giúp bạn gửi gắm những thông điệp yêu thương và tạo nên những kỷ
            niệm đáng nhớ.
          </p>
          <Button
            variant='outline'
            size='lg'
            className='border-flora-primary text-flora-primary hover:bg-flora-primary/10'
          >
            <Link href='/about'>Tìm hiểu thêm</Link>
          </Button>
        </div>
      </section>
    </div>
  );
}
