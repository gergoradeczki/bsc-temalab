import {
    Button,
    Card,
    CardActions,
    CardContent,
    IconButton,
    MenuItem,
    Stack,
    TextField,
    Typography
} from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import CloseIcon from '@mui/icons-material/Close';
import DoneIcon from '@mui/icons-material/Done';
import React from "react";
import {ITodo, TodoState, TodoStateToLabel} from "../../../types";

interface TodoProps extends ITodo {
    onClick: (action: number, position: number, newData?: ITodo) => void
}

interface TodoStates {
    edit: boolean
    name: string
    description: string
    deadline: Date
    state: TodoState
}

class Todo extends React.Component<TodoProps, TodoStates> {

    constructor(props: TodoProps) {
        super(props);
        this.getStateColor = this.getStateColor.bind(this)
        this.handleAbortButton = this.handleAbortButton.bind(this)
        this.handleSaveButton = this.handleSaveButton.bind(this)
        this.state = {
            edit: false,
            name: this.props.name,
            description: this.props.description,
            deadline: this.props.deadline,
            state: this.props.state
        }
    }

    getStateColor(state: TodoState): string {
        switch (state) {
            case TodoState.PendingState:
                return "text.primary"
            case TodoState.ProgressState:
                return "primary.main"
            case TodoState.DoneState:
                return "success.main"
            case TodoState.PostponedState:
                return "error.main"
        }
    }

    handleSaveButton() {
        let newData: ITodo = {
            id: this.props.id,
            column_id: this.props.column_id,
            position: this.props.position,
            name: this.state.name,
            description: this.state.description,
            deadline: this.state.deadline,
            state: this.state.state
        }
        this.props.onClick(3, this.props.position, newData)
        this.setState({edit: false})
    }

    handleAbortButton() {
        this.setState({
            edit: false,
            name: this.props.name,
            description: this.props.description,
            deadline: this.props.deadline,
            state: this.props.state
        })
    }

    render() {
        return (
            <Card sx={{ my: 2, width: 275 }} elevation={3} >
                <CardContent>
                    {!this.state.edit &&
                        <>
                            <Typography variant="h5" component="div" sx={{color: this.getStateColor(this.state.state)}}>{this.state.name}</Typography>
                            <Typography variant="caption">{this.state.deadline.toLocaleString()}</Typography>
                            <Typography variant="body2" color="text.secondary">{this.state.description}</Typography>
                            {/*<Typography variant="caption">id: {this.props.id}, c_id: {this.props.column_id}, pos: {this.props.position}, state: {this.props.state.toString()}</Typography>*/}
                        </>
                    }
                    {this.state.edit &&
                    <>
                        <Stack spacing={2}>
                            <TextField
                                id="todo-name"
                                label="Név"
                                variant="outlined"
                                defaultValue={this.state.name}
                                InputLabelProps={{
                                    shrink: true,
                                }}
                                onChange={(e) => this.setState({name: e.target.value})}
                            />
                            <TextField
                                id="todo-description"
                                label="Leírás"
                                variant="outlined"
                                defaultValue={this.state.description}
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
                                {TodoStateToLabel.map((option) => (
                                    <MenuItem key={option.value} value={option.value}>
                                        {option.label}
                                    </MenuItem>
                                ))}
                            </TextField>
                        </Stack>
                    </>
                    }
                </CardContent>
                <CardActions sx={{ display: 'flex', justifyContent: 'space-around' }}>
                    {!this.state.edit &&
                        <>
                            <IconButton color="primary" onClick={() => this.setState({edit: true})}>
                                <EditIcon/>
                            </IconButton>
                            <IconButton color="primary" onClick={() => this.props.onClick(0, this.props.position)}>
                                <ArrowUpwardIcon/>
                            </IconButton>
                            <IconButton color="primary" onClick={() => this.props.onClick(1, this.props.position)}>
                                <ArrowDownwardIcon/>
                            </IconButton>
                            <IconButton color="primary" onClick={() => this.props.onClick(2, this.props.position)}>
                                <DeleteIcon/>
                            </IconButton>
                        </>
                    }
                    {this.state.edit &&
                        <>
                            <Button color="error" endIcon={<CloseIcon/>} onClick={() => this.handleAbortButton()}>Elvetés</Button>
                            <Button color="success" endIcon={<DoneIcon/>} onClick={() => this.handleSaveButton()}>Mentés</Button>
                        </>
                    }
                </CardActions>
            </Card>
        )
    }
}

export { Todo, TodoState }