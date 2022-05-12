import memberMutation from './member';
import subscribeMutation from './subscribe';

const configureMutations = (
    queryClient,
    queryConfig,
) => {
    memberMutation(queryClient, queryConfig)
    subscribeMutation(queryClient,queryConfig)
}

export default configureMutations;