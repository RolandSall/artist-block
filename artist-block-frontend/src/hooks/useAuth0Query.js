import { useAuth0 } from '@auth0/auth0-react';
import { useCallback } from 'react';

export const useAuth0Query = () => {
    const { getAccessTokenSilently, isAuthenticated } = useAuth0();

    // memoized the function, as otherwise if the hook is used inside a useEffect, it will lead to an infinite loop
    const memoizedFn = useCallback(
        async () => {
            const accessToken = await getAccessTokenSilently({ audience: process.env.REACT_APP_AUTH0_AUDIENCE })

            return accessToken

        },
        [isAuthenticated, getAccessTokenSilently]
    );
    return {
        token: memoizedFn
    };
};

export default useAuth0Query;