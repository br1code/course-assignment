import { useState, useEffect } from 'react';
import { Course } from '@/types';
import { searchCourses } from '@/api';

export const useCourses = () => {
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchCourses = async (description = '') => {
    try {
      setLoading(true);
      const fetchedCourses = await searchCourses(description);
      setCourses(fetchedCourses);
      setError(null);
    } catch (err) {
      setError('Failed to fetch courses');
      console.log(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCourses();
  }, []);

  return {
    courses,
    loading,
    error,
    fetchCourses,
  };
};
