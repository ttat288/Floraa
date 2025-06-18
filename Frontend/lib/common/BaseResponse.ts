import * as z from "zod"

// Define a common interface for BaseResponse schema
export const BaseResponse = z.object({
  isSuccess: z.boolean(),
  title: z.string(),
  status: z.number(),
  errors: z.any().nullable(),
  data: z.unknown().nullable(),
})

export type BaseResponseType = z.infer<typeof BaseResponse>

// Define the response type for a successful API call
export interface SuccessResponse<T = null> extends BaseResponseType {
  isSuccess: true
  data: T // Use generic to allow different types of success data
  errors: null
}

// Define the response type for an error API call
export interface ErrorResponse extends BaseResponseType {
  isSuccess: false
  data: null
  errors: Record<string, string[]> // Specific error structure (could be a more detailed type depending on your use case)
}

// Union type for both success and error responses
export type ApiResponse<T = null> = SuccessResponse<T> | ErrorResponse
