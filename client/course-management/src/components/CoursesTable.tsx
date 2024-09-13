import { Course } from '@/types';
import { FC } from 'react';
import { FaTrash, FaSpinner } from 'react-icons/fa';

interface CoursesTableProps {
  courses: Course[];
  onDelete: (id: number) => void;
  loading: boolean;
  error: string | null;
}

const tableCellClass = 'border border-gray-300 px-4 py-4';
const tableHeaderClass = 'border border-gray-300 px-4 py-2';
const tableContainerClass = 'overflow-x-auto max-w-5xl mx-auto mt-8';
const spinnerClass = 'animate-spin text-2xl text-blue';
const centerTextClass = 'text-center py-4';

const CoursesTable: FC<CoursesTableProps> = ({
  courses,
  onDelete,
  loading,
  error,
}) => {
  if (loading) {
    return (
      <div className="flex justify-center py-4">
        <FaSpinner className={spinnerClass} />
      </div>
    );
  }

  if (error) {
    return <p className="text-center text-red-500">{error}</p>;
  }

  if (courses.length === 0) {
    return <p className={centerTextClass}>No courses found</p>;
  }

  return (
    <div className={tableContainerClass}>
      <table className="table-auto w-full border-collapse border border-gray-300">
        <thead>
          <tr className="bg-blue text-white">
            <th className={tableHeaderClass}>Id</th>
            <th className={tableHeaderClass}>Subject</th>
            <th className={tableHeaderClass}>Course Number</th>
            <th className={tableHeaderClass}>Description</th>
            <th className={tableHeaderClass}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {courses.map((course) => (
            <tr key={course.id} className="text-center">
              <td className={tableCellClass}>{course.id}</td>
              <td className={tableCellClass}>{course.subject}</td>
              <td className={tableCellClass}>{course.courseNumber}</td>
              <td className={tableCellClass}>{course.description}</td>
              <td className={tableHeaderClass}>
                <button
                  className="text-red-600 hover:text-red-700"
                  onClick={() => onDelete(course.id)}
                  aria-label="Delete course"
                >
                  <FaTrash size={18} />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default CoursesTable;
