import {getReasonPhrase, StatusCodes} from 'http-status-codes';
import {QueryClient, QueryClientProvider, useMutation} from 'react-query';
import {ReactQueryDevtools} from 'react-query/devtools';
import {CACHE_TIME_MILLISECONDS, STALE_TIME_MILLISECONDS,} from './config/constants';
import configureHooks from './hooks';
import configureMutations from './mutations';

// Query client retry function decides when and how many times a request should be retried
const retry = (failureCount, error) => {
    // do not retry if the request was not authorized
    // the user is probably not signed in
    if (error.name === getReasonPhrase(StatusCodes.UNAUTHORIZED)) {
        return 0;
    }
    return failureCount;
};

const configureQueryClient = (config) => {

    const baseConfig = {
        API_HOST:
            config?.API_HOST ||
            process.env.REACT_APP_API_HOST ||
            'http://localhost:5111/api/',
        keepPreviousData: config?.keepPreviousData || false,
    };

    const queryClient = new QueryClient({
        defaultOptions: {
            queries: {
                refetchOnWindowFocus: config?.refetchOnWindowFocus || false,
            },
        },
    });

    const queryConfig = {
        ...baseConfig,
        staleTime: config?.staleTime || STALE_TIME_MILLISECONDS,
        cacheTime: config?.cacheTime || CACHE_TIME_MILLISECONDS,
        retry,
    };

    const hooks = configureHooks(queryClient, queryConfig);

    configureMutations(queryClient, queryConfig);

    return {
        queryClient,
        QueryClientProvider,
        hooks,
        useMutation,
        ReactQueryDevtools,
    };
};

export default configureQueryClient
