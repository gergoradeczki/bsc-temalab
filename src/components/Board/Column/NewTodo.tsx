import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    MenuItem,
    Stack,
    TextField
} from "@mui/material";
import React from "react";
import AddIcon from "@mui/icons-material/Add";
import {TodoState} from "./Todo";

interface NewTodoProps {
    onClick: (name: string, description: string, deadline: Date, state: TodoState) => void
}

interface NewTodoStates {
    isDialogOpen: boolean
    name: string
    description: string
    deadline: Date
    state: TodoState
}

const states = [
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
];

class NewTodo extends React.Component<NewTodoProps, NewTodoStates> {
    constructor(props: NewTodoProps) {
        super(props)
        this.handleDialogOpen = this.handleDialogOpen.bind(this)
        this.handleDialogClose = this.handleDialogClose.bind(this)
        this.handleDialogAddBtn = this.handleDialogAddBtn.bind(this)
        this.state = {
            isDialogOpen: false,
            name: "",
            description: "",
            deadline: new Date(),
            state: TodoState.PendingState
        }
    }

    handleDialogClose() {
        this.setState({
            isDialogOpen: false
        });
    }

    handleDialogAddBtn() {
        this.props.onClick(this.state.name, this.state.description, this.state.deadline, this.state.state)
        this.handleDialogClose()
    }

    handleDialogOpen() {
        this.setState({
            isDialogOpen: true,
            name: "",
            description: "",
            deadline: new Date(),
            state: TodoState.PendingState
        });
    }

    render() {
        return (
            <>
                <Button variant="contained" endIcon={<AddIcon />} sx={{width: 275}} onClick={this.handleDialogOpen}>
                    Új teendő
                </Button>
                <Dialog open={this.state.isDialogOpen} onClose={this.handleDialogClose}>
                    <DialogTitle>Új teendő felvétele</DialogTitle>
                    <DialogContent>
                        <Box component="form" sx={{'& > :not(style)': { m: 1, width: '25ch' },}}>
                            <Stack spacing={2}>
                                <TextField
                                    id="todo-name"
                                    label="Név"
                                    variant="outlined"
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    onChange={(e) => this.setState({name: e.target.value})}
                                />
                                <TextField
                                    id="todo-description"
                                    label="Leírás"
                                    variant="outlined"
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    onChange={(e) => this.setState({description: e.target.value})}
                                />
                                <TextField
                                    id="todo-deadline"
                                    label="Határidő"
                                    type="datetime-local"
                                    defaultValue={this.state.deadline.toISOString().substring(0, 16)}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                    onChange={(e) => this.setState({deadline: new Date(e.target.value)})}
                                />
                                <TextField
                                    id="todo-state"
                                    select
                                    label="Select"
                                    value={this.state.state}
                                    onChange={(e) => this.setState({state: parseInt(e.target.value)})}
                                >
                                    {states.map((option) => (
                                    <MenuItem key={option.value} value={option.value}>
                                        {option.label}
                                    </MenuItem>
                                    ))}
                                </TextField>
                            </Stack>
                        </Box>
                    </DialogContent>
                    <DialogActions>
                        <Button color="secondary" onClick={this.handleDialogClose}>Mégsem</Button>
                        <Button color="primary" variant="contained" onClick={this.handleDialogAddBtn}>Hozzáadás</Button>
                    </DialogActions>
                </Dialog>
            </>
        )
    }
}

export { NewTodo }