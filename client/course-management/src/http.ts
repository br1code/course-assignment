import { ZodSchema } from 'zod';

const API_URL =
  typeof window === 'undefined'
    ? process.env.API_URL // Server-side
    : process.env.NEXT_PUBLIC_API_URL; // Client-side

export async function fetchData<T>(
  url: string,
  schema: ZodSchema<T>
): Promise<T> {
  try {
    const response = await fetch(`${API_URL}/${url}`);

    if (!response.ok) {
      throw new Error('Failed to fetch data');
    }

    const data = await response.json();
    return schema.parse(data);
  } catch (error) {
    throw new Error(`Failed to fetch data: ${(error as Error).message}`);
  }
}

export async function postData<T, U>(
  url: string,
  schema: ZodSchema<T> | null,
  data: U
): Promise<T | void | { errors: string[] }> {
  try {
    const response = await fetch(`${API_URL}/${url}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      if (response.status === 400) {
        const errorResponse = await response.json();
        return { errors: errorResponse.errors };
      }
      throw new Error('Failed to submit data');
    }

    if (!schema) {
      return;
    }

    const responseData = await response.json();
    return schema.parse(responseData);
  } catch (error) {
    throw new Error(`Failed to submit data: ${(error as Error).message}`);
  }
}

export async function deleteData(url: string): Promise<void> {
  try {
    const response = await fetch(`${API_URL}/${url}`, {
      method: 'DELETE',
    });

    if (!response.ok) {
      throw new Error('Failed to delete data');
    }
  } catch (error) {
    throw new Error(`Failed to delete data: ${(error as Error).message}`);
  }
}
