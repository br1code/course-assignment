'use client';

import { useForm } from 'react-hook-form';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { createCourse } from '@/api';
import Notification from '@/components/Notification';

interface CourseFormValues {
  subject: string;
  courseNumber: string;
  description: string;
}

const CreateCourseForm = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<CourseFormValues>();
  const router = useRouter();
  const [apiErrors, setApiErrors] = useState<string[]>([]);

  const onSubmit = async (data: CourseFormValues) => {
    try {
      const response = await createCourse(data);

      if (response && 'errors' in response) {
        setApiErrors(response.errors);
      } else {
        reset();
        router.push('/');
      }
    } catch (error) {
      console.error('Failed to create course:', error);
    }
  };

  return (
    <>
      <Notification messages={apiErrors} onClose={() => setApiErrors([])} />
      <form
        onSubmit={handleSubmit(onSubmit)}
        className="bg-white p-8 rounded shadow-lg max-w-lg w-full"
      >
        <h1 className="text-4xl font-bold mb-6 text-center">Create Course</h1>

        <div className="mb-4">
          <label className="block text-gray-700">Subject</label>
          <input
            {...register('subject', { required: 'Subject is required' })}
            className="w-full px-4 py-2 border rounded-md"
            placeholder="Enter subject"
          />
          {errors.subject && (
            <p className="text-red-500 text-sm">{errors.subject.message}</p>
          )}
        </div>

        <div className="mb-4">
          <label className="block text-gray-700">Course Number</label>
          <input
            {...register('courseNumber', {
              required: 'Course Number is required',
              pattern: {
                value: /^[0-9]{3}$/,
                message:
                  'Course Number must be a three-digit, zero-padded integer like "033"',
              },
            })}
            className="w-full px-4 py-2 border rounded-md"
            placeholder="Enter course number"
          />
          {errors.courseNumber && (
            <p className="text-red-500 text-sm">
              {errors.courseNumber.message}
            </p>
          )}
        </div>

        <div className="mb-6">
          <label className="block text-gray-700">Description</label>
          <textarea
            {...register('description', {
              required: 'Description is required',
            })}
            className="w-full px-4 py-2 border rounded-md"
            placeholder="Enter course description"
          />
          {errors.description && (
            <p className="text-red-500 text-sm">{errors.description.message}</p>
          )}
        </div>

        <div className="flex justify-between">
          <button
            type="button"
            onClick={() => router.push('/')}
            className="bg-gray-300 text-gray-700 px-4 py-2 rounded-md hover:bg-gray-400"
          >
            Go Back
          </button>
          <button
            type="submit"
            className="bg-blue text-white px-4 py-2 rounded-md hover:bg-blue-600"
          >
            Create
          </button>
        </div>
      </form>
    </>
  );
};

export default CreateCourseForm;
