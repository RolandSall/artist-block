import configureAuthTestHooks from './authTest';
import configureMember from './member';
import configureSearch from './search';

export default (
    queryClient,
    queryConfig,
    token
) => {
    return {
        ...configureAuthTestHooks(queryClient,queryConfig,token),
        ...configureMember(queryClient,queryConfig,token),
        ...configureSearch(queryClient,queryConfig, token)
    }
}