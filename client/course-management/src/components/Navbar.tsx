import Link from 'next/link';

export default function Navbar() {
  return (
    <nav className="bg-blue text-white px-6 py-4 flex justify-between items-center">
      <Link href="/" className="text-xl font-semibold">
        Course Management
      </Link>
      <Link
        href="/new"
        className="bg-white text-blue px-4 py-2 rounded-md font-medium"
      >
        Create Course
      </Link>
    </nav>
  );
}
