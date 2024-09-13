import { deleteData, fetchData, postData } from './http';
import { Course, coursesSchema } from './types';

export const searchCourses = (description = ''): Promise<Course[]> => {
  const searchQuery = description ? `?description=${description}` : '';
  return fetchData(`courses${searchQuery}`, coursesSchema);
};

export const deleteCourse = (id: number): Promise<void> => {
  return deleteData(`courses/${id}`);
};

export const createCourse = (
  course: Omit<Course, 'id'>
): Promise<void | { errors: string[] }> => {
  return postData('courses', null, course);
};
