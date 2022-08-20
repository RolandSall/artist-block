import {  useQuery } from 'react-query';
import * as Api from '../api';
import {PRIVATE_AUTH_KEY, PUBLIC_AUTH_KEY} from "../config/keys";

export default (
    queryClient,
    queryConfig,
) => {
    const {retry, cacheTime, staleTime} = queryConfig;
    const defaultOptions = {
        retry,
        cacheTime,
        staleTime,
    };

    const usePrivateAuth = ({isAuthenticated,token}) =>
         useQuery({
            queryKey: PRIVATE_AUTH_KEY,
            queryFn: async () =>
                Api.getPrivate(await token,queryConfig).then((data) => data),
             enabled: isAuthenticated,
             ...defaultOptions,
         });

    const usePublicAuth = () =>
         useQuery({
            queryKey: PUBLIC_AUTH_KEY,
            queryFn: () =>
                Api.getPublic(queryConfig).then((data) => data),
            ...defaultOptions,
         });

    return {
        usePrivateAuth,
        usePublicAuth
    }
}
