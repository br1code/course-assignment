import { z } from 'zod';

export const courseSchema = z.object({
  id: z.number(),
  subject: z.string(),
  courseNumber: z.string(),
  description: z.string(),
});

export const coursesSchema = z.array(courseSchema);

export type Course = z.infer<typeof courseSchema>;
