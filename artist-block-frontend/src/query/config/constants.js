import { StatusCodes } from 'http-status-codes';
// React Query Configs

// time during which cache entry is never refetched
export const STALE_TIME_MILLISECONDS = 0; // default is 0 to always refetch, can increase to trade load against consistency
// time during which cache entry is still served while refetches are pending
export const CACHE_TIME_MILLISECONDS = 1000 * 60 * 5; // default is 5 min

export const COOKIE_SESSION_NAME = 'session';
export const SIGNED_OUT_USER = {};

export const FALLBACK_TO_PUBLIC_FOR_STATUS_CODES = [
    StatusCodes.UNAUTHORIZED,
    StatusCodes.FORBIDDEN,
];
