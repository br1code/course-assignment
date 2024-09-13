'use client';

import { FC, useState } from 'react';
import { deleteCourse } from '@/api';
import { useCourses } from '@/hooks/useCourses';
import CoursesTable from '@/components/CoursesTable';
import SearchBar from '@/components/SearchBar';
import ConfirmationModal from '@/components/ConfirmationModal';

const Courses: FC = () => {
  const { courses, loading, error, fetchCourses } = useCourses();

  const [courseToDelete, setCourseToDelete] = useState<number | null>(null);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  const handleDeleteRequest = (id: number) => {
    setCourseToDelete(id);
    setIsModalOpen(true);
  };

  const handleConfirmDelete = async () => {
    if (courseToDelete !== null) {
      try {
        await deleteCourse(courseToDelete);
        fetchCourses();
        setCourseToDelete(null);
        setIsModalOpen(false);
      } catch (error) {
        console.error('Failed to delete course', error);
      }
    }
  };

  const handleCancelDelete = () => {
    setCourseToDelete(null);
    setIsModalOpen(false);
  };

  return (
    <>
      <h1 className="text-4xl text-center font-bold my-6">Courses</h1>
      <SearchBar onSearch={fetchCourses} />
      <CoursesTable
        courses={courses}
        onDelete={handleDeleteRequest}
        loading={loading}
        error={error}
      />
      <ConfirmationModal
        isOpen={isModalOpen}
        onConfirm={handleConfirmDelete}
        onCancel={handleCancelDelete}
      />
    </>
  );
};

export default Courses;
