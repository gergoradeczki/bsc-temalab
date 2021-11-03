export enum TodoState {
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
}

const TodoStateToLabel = [
    {
        value: TodoState.PendingState,
        label: 'Függőben',
    },
    {
        value: TodoState.ProgressState,
        label: 'Folyamatban',
    },
    {
        value: TodoState.DoneState,
        label: 'Kész',
    },
    {
        value: TodoState.PostponedState,
        label: 'Elhalasztva',
    },
]

export {TodoStateToLabel}