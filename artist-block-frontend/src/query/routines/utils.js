const createRoutine = (type) => ({
    TRIGGER: `${type}/TRIGGER`,
    REQUEST: `${type}/REQUEST`,
    FAILURE: `${type}/FAILURE`,
    SUCCESS: `${type}/SUCCESS`,
    FULFILL: `${type}/FULFILL`,
});

export default createRoutine;
