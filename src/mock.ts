enum TodoState {
    PendingState,
    ProgressState,
    DoneState,
    PostponedState
}

export interface IColumn {
    id: number
    name: string
}

export interface ITodo {
    id: number
    column_id: number
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

let Columns: Array<IColumn> = [
    {
        id: 0,
        name: "Column #1"
    },
    {
        id: 1,
        name: "Column #2"
    },
    {
        id: 2,
        name: "Column #3"
    },
    {
        id: 3,
        name: "Column #4"
    }
] as IColumn[]

let TodoItemsForColumns: Array<ITodo> = [
    {
        id: 0,
        column_id: 0,
        position: 0,
        name: "Pending Todo",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.PendingState
    },
    {
        id: 1,
        column_id: 1,
        position: 0,
        name: "Progressing Todo",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.ProgressState
    },
    {
        id: 2,
        column_id: 2,
        position: 0,
        name: "Done Todo",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.DoneState
    },
    {
        id: 3,
        column_id: 3,
        position: 0,
        name: "Postponed Todo",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.PostponedState
    },
    {
        id: 4,
        column_id: 0,
        position: 1,
        name: "Done Todo",
        deadline: new Date(),
        description: "Demo Description",
        state: TodoState.DoneState
    },
] as ITodo[]

// eslint-disable-next-line @typescript-eslint/no-unused-vars
let TodoItemsEmpty: Array<ITodo> = [

] as ITodo[]

export {Columns, TodoItemsForColumns, TodoState}