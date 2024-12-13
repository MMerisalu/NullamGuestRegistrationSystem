interface IButton {
    to: string,
    children : string,
    //onClick: (to: string) => void
}

const Button = ({to, children}: IButton) => {
    return (
      <button className="btn btn-primary" onClick={() => window.location.href=to}>{children}</button>
    )
  }
  
  export default Button