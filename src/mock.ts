enum TodoState {
    PendingState,
    ProgressState,
    DoneState,
    PostponedState
}

export interface ITodo {
    id: number
    position: number
    name: string
    deadline: Date
    description: string
    state: TodoState
};

// eslint-disable-next-line @typescript-eslint/no-unused-vars
let TodoItemsUnordered: Array<ITodo> = [
    {
        id: 1,
        position: 0,
        name: "Demo Name",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.PendingState
    },
    {
        id: 3,
        position: 2,
        name: "Demo Name",
        deadline: new Date(),
        description: "Demo Description Demo Description Demo Description Demo Description Demo Description Demo Description ",
        state: TodoState.PendingState
    },
    {
        id: 5,
        position: 3,
        name: "Demo Name",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.PendingState
    },
    {
        id: 6,
        position: 5,
        name: "Demo Name",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.PendingState
    },
    {
        id: 2,
        position: 1,
        name: "Demo Name",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.PendingState
    },
] as ITodo[]

let TodoItemsEmpty: Array<ITodo> = [

] as ITodo[]

export let TodoItems = TodoItemsEmpty